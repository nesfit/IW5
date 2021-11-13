namespace CookBook.Maui.BL.Services;

public interface IDependencyInjectionService
{
    TService GetRequiredService<TService>();
    object GetRequiredService(Type type);
}
