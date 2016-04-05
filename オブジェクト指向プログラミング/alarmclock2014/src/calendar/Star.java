package calendar;

public class Star {

	/**
	 * Class of get of constellation
	 * @author T.Nozawa
	 * @param yyyy,mm,dd
	 * @return constellation
	 */

	/* private */
    private String[] starAry =
    {"みずがめ座（１月２１日～２月１８日）","うお座（２月１９日～３月２０日）","おひつじ座（３月２１日～４月１９日）","おうし座（４月２０日～５月２０日）","ふたご座（５月２１日～６月２１日）","かに座（６月２２日～７月２２日）","しし座（７月２３日～８月２２日）","おとめ座（８月２３日～９月２２）","てんびん座（９月２３日～１０月２３日）","さそり座（１０月２４日～１１月２２日）","いて座（１１月２３日～１２月２１日）","やぎ座（１２月２２日～１月２０日）"};
    private int [] star_days =
    {121,219,321,420,521,622,723,823,923,1024,1123,1222};
    private String mmdd = null;
    private String starName = null;

	private void selectStarArray(int m, int d) throws ArrayIndexOutOfBoundsException {

		String day = null;
		//To two digits by one digit
		if(d < 10){
			day = "0" + Integer.toString(d);
			mmdd = Integer.toString(m) + day;
		//for Capricorn
		}else if(m == 12 && d >= 22){
			mmdd = "120";
		//for two digits
		}else{
			mmdd = Integer.toString(m) + Integer.toString(d);
		}

		//look for a constellation
		for (int i = 0; i < star_days.length; i++){
			if (Integer.parseInt(mmdd) >= star_days[i] && Integer.parseInt(mmdd) < star_days[i+1]) {
				starName = starAry[i];
				break;
			}else{
				starName = starAry[starAry.length-1];
			}
		}
    }

	/* public */
	//constructor
	public Star(){
	}
	public Star(int m, int d){
    	selectStarArray(m, d);
	}
	//get Star
    public String getStar() {
		return starName;
    }

	//for Unit test
    public static void main(String[] args) {
    	try {
	        int month = Integer.parseInt(args[0]);
	        int day = Integer.parseInt(args[1]);
			Star star = new Star(month, day);
			System.out.println(star.getStar());
    	}
		catch(ArrayIndexOutOfBoundsException e) {
			System.out.println("An error occured");
		}
    }

}