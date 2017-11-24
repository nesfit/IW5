namespace CleanCodeSample.MathExcercise
{
    public interface IMathExcercise
    {
        char Operand { get; }
        string OperationName { get; }
        int GetCorrectResult(int leftOperand, int rightOperand);
    }
}