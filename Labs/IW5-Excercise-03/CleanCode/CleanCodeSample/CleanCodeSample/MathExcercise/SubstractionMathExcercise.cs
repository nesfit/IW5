namespace CleanCodeSample.MathExcercise
{
    class SubstractionMathExcercise : IMathExcercise
    {
        public char Operand { get; } = '-';
        public string OperationName { get; } = "odčítání";

        public int GetCorrectResult(int leftOperand, int rightOperand)
        {
            return leftOperand - rightOperand;
        }
    }
}