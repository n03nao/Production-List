package calendar;


public class DayOfWeek{

	/**
	 * Class of get of dayofweek
	 * @author T.Nozawa
	 * @param yyyy,mm,dd
	 * @return dayofweek
	 */

	/* Private */
    private String[] week = {"日曜日", "月曜日", "火曜日", "水曜日", "木曜日", "金曜日", "土曜日"};
	private int week_id;
	private void calcWeek(int y, int m, int d) throws ArithmeticException {
		try {
			CalcCalendar calccalender = new CalcCalendar();
			//for January or February
			if (m == 1 || m == 2) {
			    y = y - 1;
			    m = m + 12;
			}
			week_id = calccalender.getDayOfWeek(y, m, d);
		}
		catch (ArithmeticException e){
			System.out.println("An error occured");
		}
    }

	/* public */
	//constructor
	public DayOfWeek(){
	}
	public DayOfWeek(int y, int m, int d){
		calcWeek(y, m, d);
	}
    public String getDayOfWeek() {
		return week[week_id];
    }

	//for Unit Test
    public static void main(String[] args) {
    	try{
			DayOfWeek dayofweek
				= new DayOfWeek(Integer.parseInt(args[0]), Integer.parseInt(args[1]), Integer.parseInt(args[2]));
			System.out.println(dayofweek.getDayOfWeek());
    	}
    	catch(ArrayIndexOutOfBoundsException e){
    		System.out.println("An error occured");
    	}
    }

}
