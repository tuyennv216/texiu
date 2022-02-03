using AutoMapper;

namespace texiu.MapperProfile;

public class AutomapperProfile : Profile
{
	public AutomapperProfile()
	{
		CreateMap<Data.Model.Address, Output.Data.Address>();
		CreateMap<Data.Model.AgeRatio, Output.Data.AgeRatio>();
		CreateMap<Data.Model.Firstname, Output.Data.Firstname>();
		CreateMap<Data.Model.Middlename, Output.Data.Middlename>();
		CreateMap<Data.Model.Surname, Output.Data.Surname>();
		CreateMap<Data.Model.Passwordset, Output.Data.Passwordset>();
		CreateMap<Data.Model.Wordset, Output.Data.Wordset>();
		CreateMap<Data.Model.Zipcode, Output.Data.Zipcode>();

		CreateMap<Model.Account, Output.Model.Account>();
		CreateMap<Model.Person, Output.Model.Person>();
	}
}
