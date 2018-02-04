using System.Text;
using Xunit;

namespace DataTypes
{
  public class ParametersSample
  {
    [Fact]
    public void ParametersValueTest()
    {
      StringBuilder sb = new StringBuilder();
      this.FooValue(sb);
      Assert.Equal("test",sb.ToString());      
    }
    private void FooValue(StringBuilder fooSB)
    {
      fooSB.Append("test");
      fooSB = null;
    }

    [Fact]
    public void ParametersReferenceTest()
    {
      int x = 8;
      this.FooReference(ref x);  // Ask Foo to deal directly with x
      Assert.Equal(9, x);
    }

    private void FooReference(ref int p)
    {
      p = p + 1;                // Increment p by 1
    }

    [Fact]
    public void ParametersOutTest()
    {
      this.Split("Stevie Ray Vaughan", out var a, out var b);
      Assert.Equal("Stevie Ray", a);
      Assert.Equal("Vaughan", b);
    }

    private void Split(string name, out string firstNames, out string lastName)
    {
      int i = name.LastIndexOf(' ');
      firstNames = name.Substring(0, i);
      lastName = name.Substring(i + 1);
    }

    [Fact]
    public void ParametersParamTest()
    {
      int total = this.Sum(1, 2, 3, 4);
      Assert.Equal(10,total);
    }

    private int Sum(params int[] ints)
    {
      int sum = 0;
      for (int i = 0; i < ints.Length; i++)
        sum += ints[i]; // Increase sum by ints[i]
      return sum;
    }

    [Fact]
    public void ParametersOptionalTest()
    {
      Assert.Equal(2, this.FooOptional());
      Assert.Equal(10, this.FooOptional(10));
    }

    int FooOptional(int x = 2) { return x;}

    [Fact]
    public void ParametersNamedTest()
    {
      Assert.Equal(5, this.FooNamed());
      Assert.Equal(10, this.FooNamed(7));
      Assert.Equal(10, this.FooNamed(y:8));
      Assert.Equal(10, this.FooNamed(y:6,x:4));
      Assert.Equal(10, this.FooNamed(x:7));
    }

    int FooNamed(int x = 2, int y = 3) { return x+y; }
  }
}