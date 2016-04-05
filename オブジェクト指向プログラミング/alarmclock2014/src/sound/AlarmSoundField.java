package sound;

import java.io.File;
import java.util.Calendar;
import javax.sound.sampled.AudioFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.DataLine;
import javax.sound.sampled.SourceDataLine;

public class AlarmSoundField implements InterfaceSound {

	/**
	 * Class of music performance
	 * @author T.Nozawa
	 */

	private File soundFile;

	public void soundplay(){
			//soundFile
			String [] music = {"strings.wav","musicbox.wav"};
			Calendar nowTime = Calendar.getInstance();
			soundFile = new File(music[nowTime.get(Calendar.HOUR)%2]);
			//AudioInputStream
			try {
			    AudioInputStream audioInputStream = AudioSystem.getAudioInputStream(soundFile);
			    AudioFormat audioFormat = audioInputStream.getFormat();
			    DataLine.Info info = new DataLine.Info(SourceDataLine.class,audioFormat);
			    SourceDataLine line = (SourceDataLine) AudioSystem.getLine(info);
			    line.open(audioFormat);
			    line.start();
			    int nBytesRead = 0;
			    byte[] abData = new byte[EXTERNAL_BUFFER_SIZE];
			    while (nBytesRead != -1) {
			    	nBytesRead = audioInputStream.read(abData, 0, abData.length);
			    	if (nBytesRead >= 0) {
			    		line.write(abData, 0, nBytesRead);
			    	}
			    }
			    line.close();
			} catch (Exception e) {
				System.exit(1);

			}
	}
}