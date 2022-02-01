using System.Collections;

namespace texiu.Interface;

public interface IArrayService
{
	public T[] Shuffle<T>(T[] array);
}
