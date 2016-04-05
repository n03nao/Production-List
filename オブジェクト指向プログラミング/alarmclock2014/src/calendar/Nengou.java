package calendar;

public class Nengou {

	/**
	 * Class of get of Nengou
	 * @author T.Nozawa
	 * @param yyyy
	 * @return nengou
	 * @return year
	 */

	/* Private */
	private int nengou_id = 0;
	private int year = 0;
	private String [] nengou = {"明治","大正","昭和","平成"};

	//Nengo select
	private void nengouSelect(int seireki){

		//Algorithm
		if(seireki < 1926){
			year = seireki - 1911;
			nengou_id = 1;
		}else if(seireki < 1989){
			year = seireki - 1925;
			nengou_id = 2;
		}else{
			year = seireki - 1988;
			nengou_id = 3;
		}

	}

	/* Public */
	//constructor
	public Nengou(){
	}
	public Nengou(int seireki){
		nengouSelect(seireki);
	}
	//GetNengou
	public String getNengo() throws ArrayIndexOutOfBoundsException {
		return nengou[nengou_id];
	}
	//Get year
	public int getYear() throws ArrayIndexOutOfBoundsException {
		return year;
	}

	//For Unit Test
    public static void main(String[] args) {
    	try {
    		Nengou nengou = new Nengou(Integer.parseInt(args[0]));
    		System.out.println(nengou.getNengo());
    		System.out.println(nengou.getYear());
    	}
    	catch (ArrayIndexOutOfBoundsException e) {
    		System.out.println("An error occured");
    	}
    }

}
