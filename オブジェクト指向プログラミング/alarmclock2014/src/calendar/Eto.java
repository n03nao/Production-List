package calendar;

public class Eto {

	/**
	 * Class of get of eto
	 * @author T.Nozawa
	 * @param yyyy,mm,dd
	 * @return １２子（子・丑・寅・卯・辰・巳・午・未・申・酉・戌・亥）
	 */

	private String [] etoAry =
		{"子(ね)","丑(うし)","寅(とら)","卯(う)","辰(たつ)","巳(み)","午(うま)","未(ひつじ)","申(さる)","酉(とり)","戌(いぬ)","亥(い)"};
	private int eto_id = 0;

	//Select of eto
	private void etoSelect(int year){
		eto_id = (year + 9) % 12;
		if(eto_id == 0) {
			eto_id = 12;
		}
	}

	/**/
	//
	public Eto(){
	}
	public Eto(int y){
		etoSelect(y);
	}
	public String getEto() throws ArrayIndexOutOfBoundsException {
		return etoAry[eto_id - 1];
	}

	//for Unit Test
    public static void main(String[] args) {
    	try {
    		Eto eto = new Eto(Integer.parseInt(args[0]));
    		System.out.println(eto.getEto());
    	}
    	catch (ArrayIndexOutOfBoundsException e){
    		System.out.println("An error occured");
    	}
    }

}
