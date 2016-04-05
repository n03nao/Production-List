package clock;

import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.MediaTracker;
import java.util.Calendar;

import javax.swing.JPanel;

import sound.AlarmSoundDriver;
import sound.AlarmSoundField;
import calendar.DayOfWeek;
import calendar.Eto;
import calendar.Nengou;
import circle.CalcCircle;
import circle.SubCalcCircle;

public class ClockField extends JPanel implements InterfaceClock {

	/**
	 * Class of Drawing Clock
	 * @author T.Nozawa
	 * @param AlarmTime(hh,mm,ss)
	 */

	private int hh,mm,ss;
	private Color[] colorName =
			{Color.WHITE,Color.BLUE,Color.CYAN,Color.MAGENTA,Color.GRAY,Color.GREEN,Color.LIGHT_GRAY,Color.YELLOW,Color.ORANGE,Color.PINK,Color.RED,Color.YELLOW,
			Color.WHITE,Color.BLUE,Color.CYAN,Color.MAGENTA,Color.GRAY,Color.GREEN,Color.LIGHT_GRAY,Color.YELLOW,Color.ORANGE,Color.PINK,Color.RED,Color.YELLOW,
			Color.WHITE,Color.BLUE,Color.CYAN,Color.MAGENTA,Color.GRAY,Color.GREEN,Color.LIGHT_GRAY,Color.YELLOW,Color.ORANGE,Color.PINK,Color.RED,Color.YELLOW,
			Color.WHITE,Color.BLUE,Color.CYAN,Color.MAGENTA,Color.GRAY,Color.GREEN,Color.LIGHT_GRAY,Color.YELLOW,Color.ORANGE,Color.PINK,Color.RED,Color.YELLOW,
			Color.WHITE,Color.BLUE,Color.CYAN,Color.MAGENTA,Color.GRAY,Color.GREEN,Color.LIGHT_GRAY,Color.YELLOW,Color.ORANGE,Color.PINK,Color.RED,Color.YELLOW};
	private boolean alarmSwitch;
	private String alarmMessage;
	private int imageXpos = 240;
	private int posCenter = 250;	//Position of Center
	private int hx = 0; 			//Horizontal axis
	private int hy = 0;
	private int vx = 20;			//Vertical axis
	private int vy = 20;

	//Constructor
	public ClockField(){
		this.setSize(480,480);
		this.setBackground(Color.BLACK);
		hh=00; mm=00; ss=00;
		alarmSwitch = true;
	}
	public ClockField(int h,int m,int s){
		this.setSize(480,480);
		this.setBackground(Color.BLACK);
		hh=h;mm=m;ss=s;
		alarmSwitch = false;
	}

	//Methods of clock drawing
	public void paint(Graphics screen){
		//Double buffer
		super.paint(screen);
		//Drawing of scale
		int i = 0,j = 0,k = 0;
		for(i = 0; i < 360; i += 6){
			if(i % 5 == 0){
				j = 10;
			}else{
				j = 5;
			}
			CalcCircle cc = new CalcCircle(r, i);
			double x = cc.getXPos();
			double y = cc.getYPos();
			//Big scale
			if((i % 5) == 0){
				screen.setColor(colorName[k++]);
				screen.fillOval((int)x+posCenter, (int)y+posCenter, j, j);
			//Little scale
			}else{
				screen.setColor(colorName[k++]);
				screen.fillOval((int)x+posCenter, (int)y+posCenter, j, j);
			}
		}

		//Get the Calendar info
		Calendar nowTime = Calendar.getInstance();
		int hour = nowTime.get(Calendar.HOUR);
		int min = nowTime.get(Calendar.MINUTE);
		int sec = nowTime.get(Calendar.SECOND);

		/////Add in alarm sound
		int hour24 = nowTime.get(Calendar.HOUR_OF_DAY);
		//Time of alarm
		if(hour24==hh && min==mm && sec==ss){
			//Sound
			AlarmSoundField asf = new AlarmSoundField();
			Thread t1 = new Thread(new AlarmSoundDriver(asf));
			//Start of sound
			t1.start();
		}

		//Drawing of second hand
		double rad = (Math.PI * 2 / 60) * sec;
		SubCalcCircle sccs = new SubCalcCircle(secLen, rad  + correction);
		double x = sccs.getXPos() + posCenter;
		double y = sccs.getYPos() + posCenter;
		screen.setColor(Color.WHITE);
		for (int z=248; z<253; z++){
			screen.drawLine(z, z, (int)x, (int)y);
		}

		//Drawing of minute hand
		rad = ((Math.PI * 2 / 60) * min) + ((Math.PI * 2 / (60 * 60)) * sec);
		SubCalcCircle sccm = new SubCalcCircle(minLen, rad  + correction);
		x = sccm.getXPos() + posCenter;
		y = sccm.getYPos() + posCenter;
		screen.setColor(Color.GREEN);
		for (int z=248; z<253; z++){
			screen.drawLine(z, z, (int)x, (int)y);
		}

		//Drawing of the hour hand
		rad = ((Math.PI * 2 / 12) * (hour % 12)) + ((Math.PI * 2 / (60 * 12)) * min);
		rad += (Math.PI * 2 / (60 * 60 * 12)) * sec;
		SubCalcCircle scch = new SubCalcCircle(hourLen, rad  + correction);
		x = scch.getXPos() + posCenter;
		y = scch.getYPos() + posCenter;
		screen.setColor(Color.BLUE);
		for (int z=248; z<253; z++){
			screen.drawLine(z, z, (int)x, (int)y);
		}

		//Drawing of alarm hand
		rad = (Math.PI * 2 / 12) * (hh % 12) + (Math.PI * 2 / (60 * 12)) * mm;
		rad += (Math.PI * 2 / (60 * 60 * 12)) * ss;
		SubCalcCircle scca = new SubCalcCircle(hourLen, rad  + correction);
		x = scca.getXPos() + posCenter;
		y = scca.getYPos() + posCenter;
		screen.setColor(Color.red);
		for (int z=249; z<252; z++){
			screen.drawLine(z, z, (int)x, (int)y);
		}

		/*Add in calendar information*/
		///get of Calendar info
		int year = nowTime.get(Calendar.YEAR);
		int month = nowTime.get(Calendar.MONTH)+1;
		int day = nowTime.get(Calendar.DATE);
		///Set of font & color
		screen.setFont(new Font("Century", Font.BOLD, 25));
		screen.setColor(colorName[sec%10]);
		///nengo&eto
		Nengou nengo = new Nengou(year);
		Eto eto = new Eto(year);
		String s0 = nengo.getNengo() + nengo.getYear() + "年" + " [ " + eto.getEto() + " ] " + month + "月" + day + "日";
		screen.drawString(s0, 80, 180);
		///dayoffWeek
		DayOfWeek dayofweek = new DayOfWeek(year, month, day);
		String s5 = "< " + dayofweek.getDayOfWeek() + " >";
		screen.drawString(s5, 190, 210);
		/*Star*/
///		Star star = new Star(month, day);
///		String s6 = "< " + star.getStar() + " >";
///		screen.drawString(s6, 1, 240);

		//Add in Digital Clock
		screen.setFont(new Font("SansSerif", Font.BOLD, 35));
		screen.setColor(colorName[sec%10]);
		ViewDigitalTime vdt = new ViewDigitalTime();
		screen.drawString(vdt.getDigitalTime(hour24, min, sec), 185, 380);

		//
		if (alarmSwitch == true) {
			//alarmMessage = "If you want to set the alarm time, please set the hour, minute, and second in command line arguments.";
			alarmMessage = "アラームを使いたい場合は、コマンドライン引数より「時 分 秒」を入力してください。";
			screen.setFont(new Font("SansSerif", Font.ITALIC, 12));
			screen.drawString(alarmMessage, 5, 480);
		}

		//Animation of owankun
		try {
			Image owanImages[] = new Image[20];
			MediaTracker mTrack = new MediaTracker(this);
			java.awt.Toolkit tk = getToolkit();
			for(int z = 0; z < 20; z++){
				owanImages[z] = tk.getImage("owan" + (z+1) + ".gif");
				mTrack.addImage(owanImages[z], z);
			}
			//Horizontal movement
			if((imageXpos -= 8) <= -20){
				imageXpos = 500;
			}
			screen.drawImage(owanImages[sec % 20], imageXpos, 225, this);

			//Diagonal movement
			screen.drawImage(owanImages[sec % 20], hx - 40, hy - 40, this);
			if((hx < 0) || getWidth() < hx) {
				vx = -vx;
			}
			if(hy < 0 || getHeight() < hy) {
				vy = -vy;
			}
			hx += vx;
			hy += vy;
		} catch(Exception e) {
			System.exit(1);
		}
	}
}