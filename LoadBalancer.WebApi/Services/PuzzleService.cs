using AutoMapper;
using LoadBalancer.IdenityServer.Data.Repositories.Interfaces;
using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;
using LoadBalancer.WebApi.DTOs;
using LoadBalancer.WebApi.Services.Interfaces;
using LoadBalancer.WebApi.Enums;
using System.Diagnostics;
using LoadBalancer.WebApi.Repositories.Interfaces;
using LoadBalancer.WebApi.Data.Entities.UzerToPuzzleEntity;
using LoadBalancer.WebApi.Data;
using LoadBalancer.WebApi.Data.Entities.ListenerEntity;

namespace LoadBalancer.WebApi.Services
{
    public class PuzzleService : IPuzzleService
    {
        protected readonly IPuzzleRepository _puzzleRepository;
        protected readonly IUserToPuzzleRepository _userToPuzzleRepository;
        protected readonly IStateRepository _stateRepository;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoadBalancerDbContext _context;

        public PuzzleService(IPuzzleRepository accountRepository, IHttpContextAccessor httpContextAccessor,
            IMapper mapper, IStateRepository stateRepository, IUserToPuzzleRepository userToPuzzleRepository
            , LoadBalancerDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _puzzleRepository = accountRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
            _userToPuzzleRepository = userToPuzzleRepository;
            _context = context;
        }
        private static double[,] GenerateSymmetricalMatrix(int n)
        {
            double[,] matrix = new double[n, n];
            Random random = new Random();
            double number;

            for (int i = 0; i < n; i += 2)
            {
                for (int j = 0; j < n; j++)
                {
                    number = random.Next(1, 100);
                    matrix[i, j] = number;
                    matrix[j, i] = number;
                }
            }
            return matrix;
        }

        private static double[] GenerateRoots(int n)
        {
            double[] roots = new double[n];
            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                roots[i] = random.Next(1, 100);
            }
            return roots;
        }
        public async Task<PuzzleDto> CreatePuzzleAsync(PuzzleRequestDto dto, int port)
        {
            var listener = new Listener { Port = port, Load = dto.Dimension };
            _context.Listeners.Add(listener);
            _context.SaveChanges();
            if (dto.Dimension > 2000)
            {
                throw new ArgumentException("Too big dimension");
            }
            var puzzle = _mapper.Map<Puzzle>(dto);
            puzzle.State = await _stateRepository.GetByIdAsync((int)PuzzleState.InProgress);
            puzzle.UserId = _httpContextAccessor.HttpContext.User.Identity.Name;

            UserToPuzzle user = _userToPuzzleRepository.GetUserById(puzzle.UserId.ToString());
            if (user == null)
            {
               user = new UserToPuzzle();
               user.UserId = _httpContextAccessor.HttpContext.User.Identity.Name; 
               user.MaxCount = 15;
               await _userToPuzzleRepository.AddUserToPuzzleAsync(user);
            }
            user.MaxCount -= 1;
            await _userToPuzzleRepository.UpdatePuzzleAsync(user);

            await _puzzleRepository.AddPuzzleAsync(puzzle);

            int size = puzzle.Dimension;
            double[,] matrix=GenerateSymmetricalMatrix(size);
            double[] roots=GenerateRoots(size);
            double[,] U = new double[size, size];
            double[] results = new double[size];
            double[] y = new double[size];

            for (int i = 0; i < U.GetLength(0); i++)
            {
                for (int j = 0; j < U.GetLength(1); j++)
                {
                    U[i, j] = 0;
                }
            }

            var watch = Stopwatch.StartNew();

            //перетворюємо матрицю у верхню трикутну
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < j + 1; i++)
                {
                    double partSum = 0;
                    if (i == j)   //для ii
                    {
                        for (int k = 0; k < i; k++)
                        {
                            partSum += Math.Pow(U[k, i], 2);
                        }
                        U[i, i] = Math.Round(Math.Sqrt(matrix[i, i] - partSum));
                    }
                    else //для ij 
                    {
                        for (int k = 0; k < i; k++)
                        {
                            partSum += U[k, j] * U[k, i];
                        }
                        if (U[i, i] > 0)
                        {
                            U[i, j] = (matrix[i, j] - partSum) / U[i, i];
                        }
                    }
                }

            }

            //UT*y=roots
            for (int i = 0; i < y.GetLength(0); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    double partSum = 0;
                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            partSum += U[k, j] * y[k];

                        }
                        y[j] = (roots[j] - partSum) / U[j, j];
                    }
                }
            }

            //шукаємо у зворотньому порядку результати
            double sum = 0;
            for (int i = size - 1; i > -1; i--)
            {
                for (int j = i + 1; j < size; j++)
                {
                    sum += U[i, j] * results[j];
                }
                results[i] = (y[i] - sum) / U[i, i];
            }

            watch.Stop();
            puzzle.TimeResult = watch.Elapsed.ToString();

            puzzle.State = await _stateRepository.GetByIdAsync((int)PuzzleState.Completed);
            var newPuzzle = _mapper.Map(dto, puzzle);
            var completedPuzzle = await _puzzleRepository.UpdatePuzzleAsync(newPuzzle);

            _context.Listeners.Remove(listener);
            _context.SaveChanges();

            return _mapper.Map<PuzzleDto>(completedPuzzle);
        }

        public IEnumerable<PuzzleDto> GetPuzzlesByUser(string userId)
        {
            var userPuzzles = _puzzleRepository.GetPuzzlesByUser(userId);
            return _mapper.Map<IEnumerable<PuzzleDto>>(userPuzzles);
        }

        public PuzzleDto GetPuzzlesById(int id)
        {
            var puzzle = _puzzleRepository.GetPuzzleById(id);
            return _mapper.Map<PuzzleDto>(puzzle);
        }

        public async Task<PuzzleDto> CancelPuzzleAsync(int id)
        {
            var puzzle = _puzzleRepository.GetPuzzleById(id);

            if (puzzle == null)
            {
                throw new ArgumentException("Puzzle not found");
            }
            puzzle.State = await _stateRepository.GetByIdAsync((int)PuzzleState.Canceled);
            var canceledPuzzle = await _puzzleRepository.UpdatePuzzleAsync(puzzle);
            return _mapper.Map<PuzzleDto>(canceledPuzzle);
        }
    }
}
