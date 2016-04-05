package calendar;

public class ViewDigetalCalendar {

	public String getDigitalCalendar(int y, int m, int d) {
		if(m < 10 && d <10){
		String s1 = Integer.toString(y) + ":0" + Integer.toString(m) + ":0" + Integer.toString(d);
		return s1;
		}else if(m < 10){
		String s2 = Integer.toString(y) + ":0" + Integer.toString(m) + ":" + Integer.toString(d);
		return s2;
		}else if(d < 10){
		String s3 = Integer.toString(y) + Integer.toString(m) + ":0" + Integer.toString(d);
		return s3;
		}else{
		String s4 = Integer.toString(y) + ":0" + Integer.toString(m) + ":" + Integer.toString(d);
		return s4;
		}
	}

}
