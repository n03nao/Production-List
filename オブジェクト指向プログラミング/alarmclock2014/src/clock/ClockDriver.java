package clock;

/**
 * Class of Driver Clock
 * @author T.Nozawa
 * @param ClockField
 */

public class ClockDriver extends Thread{

	private ClockField drivenClock;

	//Constructor
	public ClockDriver(ClockField clock){
		drivenClock = clock;
	}
	//Thread
	public void run(){
		while(true){
			drivenClock.repaint();
			try{
				sleep(500);
			}catch(InterruptedException e2){}
		}
	}

}
