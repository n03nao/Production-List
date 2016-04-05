package calendar;

public class Jewel {

	/**
	 * Class of get of Birthstone
	 * @author T.Nozawa
	 * @param yyyy
	 * @return Birthstone
	 */

	/* Birthstone */
    private String[] birthJewel =
    {"１月（ガーネット）","２月（アメシスト）","３月（アクアマリーン）","４月（ダイヤモンド）","５月（エメラルド）","６月（パール）","７月（ルビー）"
    ,"８月（ペリドット）","９月（サファイア）","１０月（オパール）","１１月（トパーズ）","１２月（ターコイズ）"};

	private int month_id;
	private void selectBirthJewel(int m) {
		month_id = m;
    }

	/* public */
	//constructor
	public Jewel(){
	}
	public Jewel(int mm){
		selectBirthJewel(mm);
	}
    public String getBirthJewel() throws ArrayIndexOutOfBoundsException {
		return birthJewel[month_id - 1];
    }

	//for Unit test
    public static void main(String[] args) {
    	try {
	        int month = Integer.parseInt(args[0]);
			Jewel jewel = new Jewel(month);
			System.out.println(jewel.getBirthJewel());
    	}
		catch(ArrayIndexOutOfBoundsException e) {
			System.out.println("An error occured");
		}
    }

}
