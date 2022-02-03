using Newtonsoft.Json;
using texiu.Interface;
using texiu.Data.Model;

namespace texiu.Service;

public class DataService : IDataService
{
	private readonly IArrayService _arrayService;

	private readonly Address[] _addresses;
	private readonly string[] _mergedAddresses;

	private readonly AgeRatio[] _ageRatio;
	private readonly float[] _ageChances;
	private int[] _ageChancesIndexes;

	private readonly Firstname[] _firstnames;
	private readonly float[] _firstnamesChances;
	private int[] _firstnamesChancesIndexes;

	private readonly Middlename[] _middlenames;
	private readonly float[] _middlenamesChances;
	private int[] _middlenamesChancesIndexes;

	private readonly Surname[] _surnames;
	private readonly float[] _surnamesChances;
	private int[] _surnamesChancesIndexes;

	private readonly Zipcode[] _zipcodes;
	private readonly float[] _zipcodesChances;
	private int[] _zipcodesChancesIndexes;

	private readonly Wordset _wordset;

	private readonly Passwordset[] _passwordsets;
	private readonly float[] _passwordsetsChances;
	private int[] _passwordsetsChancesIndexes;

	// update reference data each hour
	private DateTime _lastUpdateTime = DateTime.Now;
	private readonly TimeSpan _updateTimespan = TimeSpan.FromHours(1);

	public DataService(IArrayService arrayService)
	{
		_arrayService = arrayService;

		// Load addresses
		string addressFile = File.ReadAllText(@"Data/address.json");
		_addresses = JsonConvert.DeserializeObject<Address[]>(addressFile) ?? Array.Empty<Address>();
		// Merge addresses
		var mergedAddresses = new List<string>();
		for (var i = 0; i < _addresses.Length; i++)
		{
			for (var j = 0; j < _addresses[i].Districts.Length; j++)
			{
				// District, Province
				mergedAddresses.Add($"{_addresses[i].Districts[j]}, {_addresses[i].Province}");
			}
		}
		_mergedAddresses = mergedAddresses.ToArray();

		// Load age ratio
		string ageRatioFile = File.ReadAllText(@"Data/age-ratio.json");
		_ageRatio = JsonConvert.DeserializeObject<AgeRatio[]>(ageRatioFile) ?? Array.Empty<AgeRatio>();
		_ageChances = _ageRatio.Select(x => x.Chance).ToArray();
		_ageChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _ageChances));

		// Load first name
		string firstNameFile = File.ReadAllText(@"Data/first-name.json");
		_firstnames = JsonConvert.DeserializeObject<Firstname[]>(firstNameFile) ?? Array.Empty<Firstname>();
		_firstnamesChances = _firstnames.Select(x => x.Chance).ToArray();
		_firstnamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _firstnamesChances));

		// Load middle name
		string middlenamesFile = File.ReadAllText(@"Data/middle-name.json");
		_middlenames = JsonConvert.DeserializeObject<Middlename[]>(middlenamesFile) ?? Array.Empty<Middlename>();
		_middlenamesChances = _middlenames.Select(x => x.Chance).ToArray();
		_middlenamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _middlenamesChances));

		// Load sur name
		string surnameFile = File.ReadAllText(@"Data/sur-name.json");
		_surnames = JsonConvert.DeserializeObject<Surname[]>(surnameFile) ?? Array.Empty<Surname>();
		_surnamesChances = _surnames.Select(x => x.Chance).ToArray();
		_surnamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _surnamesChances));

		// Load zipcode
		string zipcodeFile = File.ReadAllText(@"Data/zipcode.json");
		_zipcodes = JsonConvert.DeserializeObject<Zipcode[]>(zipcodeFile) ?? Array.Empty<Zipcode>();
		_zipcodesChances = _zipcodes.Select(x => x.Chance).ToArray();
		_zipcodesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _zipcodesChances));

		// Load wordset
		string wordsetFile = File.ReadAllText(@"Data/word-set.json");
		_wordset = JsonConvert.DeserializeObject<Wordset>(wordsetFile) ?? new Wordset();

		// Load passwordset
		string passwordsetFile = File.ReadAllText(@"Data/password-set.json");
		_passwordsets = JsonConvert.DeserializeObject<Passwordset[]>(passwordsetFile) ?? Array.Empty<Passwordset>();
		_passwordsetsChances = _passwordsets.Select(x => x.Chance).ToArray();
		_passwordsetsChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _passwordsetsChances));

	}

	#region address
	public Address[] GetAddresses()
	{
		return _addresses;
	}

	public string[] GetMergedAddresses()
	{
		return _mergedAddresses;
	}
	#endregion

	#region age
	public AgeRatio[] GetAgeRatio()
	{
		return _ageRatio;
	}

	public int[] GetAgeChancesIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_ageChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _ageChances));
		}

		return _ageChancesIndexes;
	}
	#endregion

	#region first name
	public Firstname[] GetFirstnames()
	{
		return _firstnames;
	}

	public int[] GetFirstnameIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_firstnamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _firstnamesChances));
		}

		return _firstnamesChancesIndexes;
	}
	#endregion

	#region middle name
	public Middlename[] GetMiddlenames()
	{
		return _middlenames;
	}

	public int[] GetMiddlenameIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_middlenamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _middlenamesChances));
		}

		return _middlenamesChancesIndexes;
	}
	#endregion

	#region sur name
	public Surname[] GetSurnames()
	{
		return _surnames;
	}

	public int[] GetSurnameIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_surnamesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _surnamesChances));
		}

		return _surnamesChancesIndexes;
	}
	#endregion

	#region zip code
	public Zipcode[] GetZipcodes()
	{
		return _zipcodes;
	}

	public int[] GetZipcodesIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_zipcodesChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _zipcodesChances));
		}

		return _zipcodesChancesIndexes;
	}
	#endregion

	#region wordset
	public Wordset GetWordset()
	{
		return _wordset;
	}
	#endregion

	#region password set
	public Passwordset[] GetPasswordsets()
	{
		return _passwordsets;
	}

	public int[] GetPasswordsetsIndexes()
	{
		var now = DateTime.Now;
		if (now - _lastUpdateTime > _updateTimespan)
		{
			_lastUpdateTime = now;
			_passwordsetsChancesIndexes = _arrayService.IndexesByRatio(100, string.Join(',', _passwordsetsChances));
		}

		return _passwordsetsChancesIndexes;
	}
	#endregion
}
