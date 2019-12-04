namespace AdventOfCode2019
{
    class Day4
    {
        public string CalcA()
        {
            int foundSol = 0;
            for(int i = 367479; i <= 893698; i++)
                if (EvalPwd(i))
                    foundSol++;
            return foundSol.ToString();
        }

        public string CalcB()
        {
            int foundSol = 0;
            for(int i = 367479; i <= 893698; i++)
                if (EvalPwd2(i))
                    foundSol++;
            return foundSol.ToString();
        }

        
        private bool EvalPwd(int pwd)
        {
            bool foundDouble = false;
            int last = pwd % 10;
            pwd /= 10;
            while (pwd > 0)
            {
                int digit = pwd % 10;
                pwd /= 10;

                if (digit > last)
                    return false;

                if (digit == last) 
                    foundDouble = true;

                last = digit;
            }

            return foundDouble;
        }
        
        private bool EvalPwd2(int pwd)
        {
            int numInRow = 0;
            bool foundDouble = false;
            int last = pwd % 10;
            pwd /= 10;
            while (pwd > 0)
            {
                int digit = pwd % 10;
                pwd /= 10;

                if (digit > last)
                    return false;

                if (digit == last)
                {
                    numInRow++;
                }
                else if (numInRow == 1)
                {
                    foundDouble = true;
                    numInRow = 0;
                }
                else
                {
                    numInRow = 0;
                }

                last = digit;
            }

            if (numInRow == 1)
                foundDouble = true;
            
            return foundDouble;
        }

    }
}