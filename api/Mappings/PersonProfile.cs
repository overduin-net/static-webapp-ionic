public class PersonProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Person, PersonDTO>()
            .Map(dest => dest.Id, src => src.RowKey);
        config.NewConfig<PersonDTO, Person>()
            .Map(dest => dest.RowKey, src => src.Id);
    }
}
