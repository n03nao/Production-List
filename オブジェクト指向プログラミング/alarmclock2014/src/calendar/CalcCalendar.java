package calendar;

public class CalcCalendar {

	/**
	 * Class of get of CalcCalendar
	 * @author T.Nozawa
	 * @param yyyy,mm,dd
	 * @return dayofweek_id
	 */

	/* Private */
	//Formula Zeller
	private int answer = 0;
	private void formulaZeller(int a, int b, int c) {
		answer = (a + a / 4 - a / 100 + a / 400 + (26 * b + 16) / 10 + c) % 7;
	}

	/* public */
	public int getDayOfWeek(int y, int m, int d){
		formulaZeller(y,m,d);
		return answer;
	}
}
