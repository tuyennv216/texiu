using texiu.Data.Model;

namespace texiu.Interface;

public interface IDataService
{
	public Address[] GetAddresses();
	public string[] GetMergedAddresses();
	
	public AgeRatio[] GetAgeRatio();
	public int[] GetAgeChancesIndexes();

	public Firstname[] GetFirstnames();
	public int[] GetFirstnameIndexes();

	public Middlename[] GetMiddlenames();
	public int[] GetMiddlenameIndexes();
	
	public Surname[] GetSurnames();
	public int[] GetSurnameIndexes();

	public Zipcode[] GetZipcodes();
	public int[] GetZipcodesIndexes();

	public Wordset GetWordset();

}
