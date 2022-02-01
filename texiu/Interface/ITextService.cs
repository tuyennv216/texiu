namespace texiu.Interface;

public interface ITextService
{
	public char ReplaceLetter(char chr, char replaceChar = '_');
	public string ReplaceLetter(string text, char replaceChar = '_');
	public string MissingChar(string text, float percent, char replaceChar = '_');
	public string MissingWord(string text, float percent, char replaceChar = '_');
}
