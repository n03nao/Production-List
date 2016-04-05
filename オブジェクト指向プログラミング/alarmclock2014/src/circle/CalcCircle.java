package circle;

public class CalcCircle extends AbstractCircle
					implements InterfacePolarCircle {
	/**
	 * Class of get of Circle
	 * @author T.Nozawa
	 * @param yyyy,mm,dd
	 * @return Answer
	 */

//	/* private */
	//Radius
	protected double radius = 0.0;
	//Degree
	protected double degree = 0.0;
	//Radian
	protected double radian = 0.0;
	//xy position
	protected double xPos = 0.0;
	protected double yPos = 0.0;
	private double area = 0.0;
	//circumference
	private double circumference = 0.0;

	//Convert degrees to radians
	private double deg2rad(){
		return Math.toRadians(degree);
	}

	/* public */
	//Overload of constructor
	public CalcCircle() {
	}
	public CalcCircle(double r){
		radius = r;
		calcArea();
		calcCircumference();
	}
	public CalcCircle(double r, int d){
		this.radius = r;
		this.degree = d;
		calcXPos();
		calcYPos();
	}
	//xpos
	private void calcXPos(){
		xPos = this.radius * Math.cos(deg2rad());
	}
	//ypos
	private void calcYPos(){
		yPos = this.radius * Math.sin(deg2rad());
	}
	//area
	private void calcArea(){
		area = radius * radius * Math.PI;
	}
	//circumference
	private void calcCircumference(){
		circumference = radius * 2 * Math.PI;
	}

	/* public */
	public double getXPos(){
		return xPos;
	}
	public double getYPos(){
		return yPos;
	}
	public double getArea(){
		return area;
	}
	public double getCircumference(){
		return circumference;
	}
}
