namespace texiu.Interface;

public interface ITextService
{
	public char ReplaceLetter(char chr);
	public string ReplaceLetter(string text);
	public string MissingChar(string text, float percent);
	public string MissingWord(string text, float percent);
}
