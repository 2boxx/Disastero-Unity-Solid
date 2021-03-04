public interface IObservable {
	void Attach(IObservator obj);
	void Detach(IObservator obj);
}

public interface IObservator {
	void Execute();
}
