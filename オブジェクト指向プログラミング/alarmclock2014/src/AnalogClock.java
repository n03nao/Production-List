
import java.awt.*;
import javax.swing.*;

import clock.*;

class AnalogClock{

	public static void main(String args[]){


		ClockField cf;

		//draw of frame
		JFrame frm = new JFrame("AlarmClock");
		JPanel pnl = new JPanel();
		//set of alarm time
		if (args.length == 0){
			cf = new ClockField();
		} else {
			int h = Integer.parseInt(args[0]);
			int m = Integer.parseInt(args[1]);
			int s = Integer.parseInt(args[2]);
			cf = new ClockField(h, m, s);
		}
		//draw of clock
		frm.add(cf, BorderLayout.CENTER);
		frm.add(pnl, BorderLayout.SOUTH);
		frm.setSize(500, 550);
		frm.setVisible(true);
		//drive of clock
		ClockDriver cd = new ClockDriver(cf);
		Thread t1 = new Thread(cd);
		t1.start();

	}
}
