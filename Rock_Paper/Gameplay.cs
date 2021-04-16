using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper
{
    class Gameplay
    {
        private List<string> _allSteps;
        private int _stepsCount;
        private readonly byte[] _random = new byte[16];

        private int _computerStep;
        private int _userStep;
        private int _avarage;

        public Gameplay(List<string> allSteps)
        {
            _allSteps = allSteps;
            _stepsCount = _allSteps.Count;
            _avarage = allSteps.Count / 2;

        }

        private string BitConvert(byte[] bc)
        {
            return BitConverter.ToString(bc).Replace("-", string.Empty);
        }

        private string GetHmac()
        {
            var hmacSha256 = new HMACSHA256(_random);
            var myHmac = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(_computerStep.ToString()));
            return BitConvert(myHmac);
        }

        private void ComputerStep()
        {
            _computerStep = RandomNumberGenerator.GetInt32(0,_allSteps.Count);
            RandomNumberGenerator.Fill(_random);
            
        }

        private void userStep()
        {
            while (true)
            {
                var input = int.Parse(Console.ReadLine());
                try
                {
                    if (input > _stepsCount || input < 0) throw new ArgumentOutOfRangeException();
                    _userStep = input;
                    return;
                }
                catch
                {
                    Console.WriteLine("Invalid input! Try again!");
                    ShowMenu();
                }
            }

        }

        private void ShowMenu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < _allSteps.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_allSteps[i]}");
            }
            Console.WriteLine("0 - Exit");
            Console.Write("Enter your move: ");
        }

        internal void Start()
        {
            ComputerStep();
            Console.WriteLine($"HMAC: {GetHmac()}");
            ShowMenu();
            userStep();
            if (_userStep == 0)
            {
                Console.WriteLine("Thank you for the game! See you!");
                return;
            }

            Console.WriteLine($"Your move: {_allSteps[_userStep - 1]}");
            Console.WriteLine($"Computer move: {_allSteps[_computerStep]}");
            ChoiceResult();
            
            Console.WriteLine($"HMAC key: {BitConvert(_random)}");
        }

        private void ChoiceResult()
        {
            _userStep -= 1;
            // var r = input - _avarage;
            var result = (_computerStep - _userStep + _avarage) % _allSteps.Count;
            if (result > _avarage)
            {
                Console.WriteLine("You lose!");
            }
            else if (result < _avarage)
            {
               Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
            
        } 
    }
}
