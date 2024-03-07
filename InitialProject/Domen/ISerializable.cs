namespace InitialProject.Domen
{
    public interface ISerializable
    {
        string[] ToCSV();
        void FromCSV(string[] values);

    }
}
