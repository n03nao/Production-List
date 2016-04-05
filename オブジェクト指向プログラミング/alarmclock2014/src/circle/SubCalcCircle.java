package circle;

public class SubCalcCircle extends CalcCircle {

	public SubCalcCircle(double r, double rad){
		radius = r;
		radian = rad;
		this.calcXPos();
		this.calcYPos();
	}
	//calcXPos
	private void calcXPos(){
		xPos = radius * Math.cos(radian);
	}
	//calcYPos
	private void calcYPos(){
		yPos = radius * Math.sin(radian);
	}

}
