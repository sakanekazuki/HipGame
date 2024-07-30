using Cysharp.Threading.Tasks;

// タイトルマネージャーのインターフェイス
public interface IGM_Title
{
	UniTask GameStart();
	void OptionOpen();
	void OptionClose();
	void CreditOpen();
}
