namespace CleanCodeSample.MathExcercise
{
    class AdditionMathExcercise : IMathExcercise
    {
        public char Operand { get; } = '+';
        public string OperationName { get; } = "sčítání";

        public int GetCorrectResult(int leftOperand, int rightOperand)
        {
            return leftOperand + rightOperand;
        }
    }
}