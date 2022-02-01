using System.Collections;
using System.Security.Cryptography;
using texiu.Interface;

namespace texiu.Service;

public class RandomService : IRandomService
{
	private readonly IArrayService _arrayService;
	public RandomService(IArrayService arrayService)
	{
		_arrayService = arrayService;
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
}
