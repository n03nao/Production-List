package sound;

public class AlarmSoundDriver extends Thread{

	/**
	 * Class of Driver Sound
	 * @author T.Nozawa
	 * @param AlarmSoundField
	 */

	private AlarmSoundField drivenSound;

	public AlarmSoundDriver(AlarmSoundField sound){
		drivenSound = sound;
	}

	public void run(){
			try{
				drivenSound.soundplay();
			}catch(Exception e){}
	}

}
