namespace CleanCodeSample.MathExcercise
{
    public interface IMathOperation
    {
        char Operand { get; }
        string OperationName { get; }
        int GetCorrectResult(int leftOperand, int rightOperand);
    }
}