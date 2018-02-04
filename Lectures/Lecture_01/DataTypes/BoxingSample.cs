using Xunit;
namespace DataTypes
{
  public class BoxingSample
  {
    [Fact]
    public void BoxingTest()
    {
      var i = 123;
      object o = i;    // Boxing
      var j = (int) o; // Unboxing
      Assert.NotSame(i,o);
      Assert.NotSame(o,j);
      Assert.NotSame(i,j);
    }
  }
}