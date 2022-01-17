using UnityEngine;

public class ObjectRewindHandler : RewindHandler {
    

    public override void Start(){
        base.Start();
        RegisterHandler();
    }

    private void RegisterHandler(){
        RewindManager rewindManager = RewindManager.instance;
        if(!rewindManager){
            Debug.LogError("CANT FIND REWIND MANAGER !");
            return;
        }

        rewindManager.RegisterRewindHandler(this);
    }

    protected override void Execute ()
		{			
			if (CancelRequested ()) {
				Reset ();
				return;
			}
		
			foreach (var command in commands[currentGroup].Reverse ()) {
				command.Undo ();
			}
				
			commands.RemoveAt (currentGroup);
			currentGroup--;
				
			if (currentGroup < 0) {
				Reset ();
				return;
			}
		}
		
		private void Reset ()
		{
			ResetFlags ();
			
			if (currentGroup < 0) {
				currentGroup = 0;
				commands.Add (new CommandGroup ());
			}
		}
		
		private void ResetFlags ()
		{
			complete = true;
			cancelRewind = false;
			rewindRequired = false;
			canRewind = true;
		}
		
}