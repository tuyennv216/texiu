using System.Collections;
using System.Security.Cryptography;
using texiu.Interface;

namespace texiu.Service;

public class RandomService : IRandomService
{
	private readonly IArrayService _arrayService;
	private readonly IDataService _dataService;
	public RandomService(IArrayService arrayService, IDataService dataService)
	{
		_arrayService = arrayService;
		_dataService = dataService;
	}
	public T RandomItem<T>(T[] items)
	{
		var rnd = RandomNumberGenerator.GetInt32(0, items.Length);
		return items[rnd];
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
		if (upper >= 100) upper = 99;
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

		var output = new string[quantity];
		for (var i = 0; i < quantity; i++)
		{
			var firstName = RandomItem(firstnames, firstnameIndexes);
			var middleName = RandomItem(middlenames, middlenameIndexes);
			var surName = RandomItem(surnames, surnamesIndexes);

			var first = RandomItem(firstName.NamesArr);
			var middle = RandomItem(middleName.NamesArr);
			var sur = RandomItem(surName.NamesArr);

			var name = $"{sur} {middle} {first}";
			output[i] = name;
		}

		return output;
	}

	public string[] RandomPassword(int length, int quantity)
	{
		return new string[] { };
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
}
