using System.Collections;
using System.Security.Cryptography;
using texiu.Interface;
using texiu.Model;

namespace texiu.Service;

public class RandomService : IRandomService
{
	private readonly IConfiguration _configuration;
	private readonly IArrayService _arrayService;
	private readonly IDataService _dataService;

	public RandomService(IConfiguration configuration, IArrayService arrayService, IDataService dataService)
	{
		_configuration = configuration;
		_arrayService = arrayService;
		_dataService = dataService;
	}

	public T RandomItem<T>(T[] items)
	{
		if (items.Length == 1) return items[0];
		var index = RandomNumberGenerator.GetInt32(0, items.Length);
		return items[index];
	}

	public char RandomItem(string str)
	{
		if (str.Length == 1) return str[0];
		var index = RandomNumberGenerator.GetInt32(0, str.Length);
		return str[index];
	}

	public T RandomItem<T>(T[] items, int lower, int upper)
	{
		var rnd = RandomNumberGenerator.GetInt32(lower, upper);
		return items[rnd];
	}

	public T RandomItem<T>(T[] items, int[] indexes)
	{
		var rnd = RandomNumberGenerator.GetInt32(0, indexes.Length);
		return items[indexes[rnd]];
	}

	public T RandomItem<T>(T[] items, int[] indexes, int lower_index, int upper_index)
	{
		var idx = RandomNumberGenerator.GetInt32(lower_index, upper_index);
		var item = items[indexes[idx]];
		return item;
	}

	public int[] RandomSplitNumber(int number, int upper)
	{
		var ints = new List<int>();
		while (number > 0)
		{
			int sub;
			if (upper >= number) sub = number;
			else sub = RandomNumberGenerator.GetInt32(1, Math.Min(upper, number));
			ints.Add(sub);
			number -= sub;
		}
		return ints.ToArray();
	}

	public string[] RandomAddress(int quantity)
	{
		var addresses = _dataService.GetMergedAddresses();
		var rndIndexes = _arrayService.ShuffleIndex(addresses);
		var output = rndIndexes.Select(x => addresses[x]).Take(quantity).ToArray();
		return output;
	}

	public int[] RandomAge(int lower, int upper, int quantity)
	{
		if (lower < 0) lower = 0;
		if (upper > 99) upper = 99;
		if (lower > upper) return Array.Empty<int>();

		var ageRatio = _dataService.GetAgeRatio();
		var ageIndexes = _dataService.GetAgeChancesIndexes();

		var output = new int[quantity];
		var i = 0;
		var taked = 0;
		while (taked < quantity)
		{
			// get range index of a random age
			var lower_index = Array.IndexOf(ageIndexes, lower / 10);
			var upper_index = Array.LastIndexOf(ageIndexes, upper / 10);
			var range = RandomItem(ageRatio, ageIndexes, lower_index, upper_index);
			if (lower < range.Max && upper > range.Min)
			{
				// get an age in the range
				var left = Math.Max(range.Min, lower);
				var right = Math.Min(range.Max, upper);
				output[taked] = RandomNumberGenerator.GetInt32(left, right);
				taked++;
			}
			i++;
		}
		return output;
	}

	public int[] RandomFlagInt(int length)
	{
		var rndBytes = RandomNumberGenerator.GetBytes(length);
		var output = new int[length];
		for (var i = 0; i < length; i++)
		{
			// bit 1 at i mod 8 of each byte
			output[i] = rndBytes[i] >> (i & 0b0111) & 1;
		}
		return output;
	}

	public int[] RandomFlagInt(int length, int bitOneLength)
	{
		var ints = new int[length];
		Array.Fill(ints, 0);
		for (var i = 0; i < bitOneLength; i++) ints[i] = 1;

		var output = _arrayService.Shuffle(ints);
		return output;
	}

	public int[] RandomIndexByRatio(int length, string ratio)
	{
		var indexes = _arrayService.IndexesByRatio(length, ratio);
		var output = indexes.Select(x => RandomItem(indexes)).ToArray();
		return output;
	}

	public string[] RandomName(int quantity)
	{
		var firstnames = _dataService.GetFirstnames();
		var middlenames = _dataService.GetMiddlenames();
		var surnames = _dataService.GetSurnames();

		var firstnameIndexes = _dataService.GetFirstnameIndexes();
		var middlenameIndexes = _dataService.GetMiddlenameIndexes();
		var surnamesIndexes = _dataService.GetSurnameIndexes();

		// length 3 (0, 1): 50 + 10, length 4 (2, 3): 15 + 15, length 5 (4, 5): 5 + 5
		var lengthIndexes = _arrayService.IndexesByRatio(100, "50,10,15,15,5,5");

		var output = new string[quantity];
		for (var i = 0; i < quantity; i++)
		{
			var firstName1 = RandomItem(firstnames, firstnameIndexes);
			var firstName2 = RandomItem(firstnames, firstnameIndexes);
			var middleName1 = RandomItem(middlenames, middlenameIndexes);
			var middleName2 = RandomItem(middlenames, middlenameIndexes);
			var middleName3 = RandomItem(middlenames, middlenameIndexes);
			var surName = RandomItem(surnames, surnamesIndexes);

			var first1 = RandomItem(firstName1.NamesArr);
			var first2 = RandomItem(firstName2.NamesArr);
			var middle1 = RandomItem(middleName1.NamesArr);
			var middle2 = RandomItem(middleName2.NamesArr);
			var middle3 = RandomItem(middleName3.NamesArr);
			var sur = RandomItem(surName.NamesArr);

			var rndLength = RandomItem(lengthIndexes);
			switch (rndLength)
			{
				case 1: // length 3
					{
						var name = $"{middle1} {middle2} {first1}";
						output[i] = name;
						break;
					}
				case 2: // length 4
					{
						var name = $"{sur} {middle1} {middle2} {first1}";
						output[i] = name;
						break;
					}
				case 3: // length 4
					{
						var name = $"{sur} {middle1} {first1} {first2}";
						output[i] = name;
						break;
					}
				case 4: // length 5
					{
						var name = $"{sur} {middle1} {middle2} {middle3} {first1}";
						output[i] = name;
						break;
					}
				case 5: // length 5
					{
						var name = $"{sur} {middle1} {middle2} {first1} {first2}";
						output[i] = name;
						break;
					}
				default: // case 0: length 3
					{
						var name = $"{sur} {middle1} {first1}";
						output[i] = name;
						break;
					}
			}
		}

		return output;
	}

	public string[] RandomPassword(int length, int quantity)
	{
		if (length < 6) length = 6;
		var passwordset = _dataService.GetPasswordsets();
		var indexes = _dataService.GetPasswordsetsIndexes();

		var output = new string[quantity];
		for (var i = 0; i < quantity; i++)
		{
			var onepassword = new char[length];
			for (var j = 0; j < length; j++)
			{
				var rndSet = RandomItem(passwordset, indexes);
				var rndChar = RandomItem(rndSet.Characters);
				onepassword[j] = rndChar;
			}
			output[i] = new string(onepassword);
		}

		return output;
	}

	public int[] RandomZipcode(int quantity)
	{
		var zipcodes = _dataService.GetZipcodes();
		var zipcodesIndexes = _dataService.GetZipcodesIndexes();
		var output = new int[quantity];
		for (var i = 0; i < quantity; i++)
		{
			var zipcode = RandomItem(zipcodes, zipcodesIndexes);
			if (zipcode.Min == zipcode.Max) output[i] = zipcode.Min;
			else output[i] = RandomNumberGenerator.GetInt32(zipcode.Min, zipcode.Max + 1);
		}
		return output;
	}

	public string[] RandomUsername(int length, int quantity)
	{
		if (length < 4) length = 4;
		var wordset = _dataService.GetWordset();

		var output = new string[quantity];
		for (var i = 0; i < quantity; i++)
		{

			// split length number to a sequence of small number
			var parts = RandomSplitNumber(length, wordset.Upper);

			// segments is an array like [s1, m2, m3 ... e1]
			var segments = new string[parts.Length];
			segments[0] = $"s{parts[0]}";
			for (var k = 1; k < parts.Length - 1; k++) segments[k] = $"m{parts[k]}";
			segments[parts.Length - 1] = $"e{parts[parts.Length - 1]}";

			string FindWord(string text)
			{
				if (wordset.Units.Contains(text))
					return RandomItem(wordset.Mapping[text]);
				else
					return string.Concat(wordset.GetItems(text).Select(x => FindWord(x)));
			}

			var word = string.Concat(segments.Select(x => FindWord(x)));
			output[i] = word;
		}

		return output;
	}

	public Account[] RandomAccounts(int quantity)
	{
		var username_len = _configuration.GetValue("custom:random:account:username_len", 8);
		var password_len = _configuration.GetValue("custom:random:account:password_len", 12);
		var usernames = RandomUsername(username_len, quantity);
		var passwords = RandomPassword(password_len, quantity);

		var output = new Account[quantity];
		for (int i = 0; i < quantity; ++i)
		{
			output[i] = new Account()
			{
				Username = usernames[i],
				Password = passwords[i],
			};
		}

		return output;
	}

	public Person[] RandomPeople(int quantity, int fromAge = 1, int toAge = 99)
	{
		var names = RandomName(quantity);
		var ages = RandomAge(fromAge, toAge, quantity);
		var addresses = RandomAddress(quantity);
		var zipcodes = RandomZipcode(quantity);

		var output = new Person[quantity];
		for (var i = 0; i < quantity; ++i)
		{
			output[i] = new Person()
			{
				Name = names[i],
				Age = ages[i],
				Address = addresses[i],
				Zipcode = zipcodes[i],
			};
		}

		return output;
	}

}
