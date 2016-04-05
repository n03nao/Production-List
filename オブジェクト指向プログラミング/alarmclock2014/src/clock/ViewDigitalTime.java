package clock;

public class ViewDigitalTime {

	public String getDigitalTime(int h, int m, int s) {
		if(m < 10 && s <10){
		String s1 = Integer.toString(h) + ":0" + Integer.toString(m) + ":0" + Integer.toString(s);
		return s1;
		}else if(m < 10){
		String s2 = Integer.toString(h) + ":0" + Integer.toString(m) + ":" + Integer.toString(s);
		return s2;
		}else if(s < 10){
		String s3 = Integer.toString(h) + ":" + Integer.toString(m) + ":0" + Integer.toString(s);
		return s3;
		}else{
		String s4 = Integer.toString(h) + ":" + Integer.toString(m) + ":" + Integer.toString(s);
		return s4;
		}
	}

}
