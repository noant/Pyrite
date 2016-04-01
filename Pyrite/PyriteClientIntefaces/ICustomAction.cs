namespace PyriteClientIntefaces
{
    public interface ICustomAction
    {
        string Do(string inputState);
        string State { get; }
        string Name { get; }

        bool AllowUserSettings { get; }

        bool BeginUserSettings();

        bool IsBusyNow { get; }

        void Refresh();
    }
}
