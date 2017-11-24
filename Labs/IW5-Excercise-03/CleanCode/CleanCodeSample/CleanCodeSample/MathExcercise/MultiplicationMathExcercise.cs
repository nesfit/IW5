namespace CleanCodeSample.MathExcercise
{
    class MultiplicationMathExcercise : IMathExcercise
    {
        public char Operand { get; } = '*';
        public string OperationName { get; } = "násobení";

        public int GetCorrectResult(int leftOperand, int rightOperand)
        {
            return leftOperand * rightOperand;
        }
    }
}