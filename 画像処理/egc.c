/* �������@�t�B���^ */
#include <windows.h>
#include	<stdio.h>
#include	"opgls.ft"

#define		MaxSiz		1024
#define		gp(y, x)	dat[(y)*wide+(x)]

unsigned  char	dat[MaxSiz*MaxSiz];
int	 			cm, wide, hite;
signed int		dw[MaxSiz][MaxSiz];//sighed�͂��Ȃ��Ă��ǂ����A�}�C�i�X�̒l����邱�Ƃ��킩��₷�����邽�߂ɂ��Ă���B
signed int		ddw;	//sighed�͂��Ȃ��Ă��ǂ����A�}�C�i�X�̒l����邱�Ƃ��킩��₷�����邽�߂ɂ��Ă���B


int		wline[3][3]={{0,-1,0},			// �G�b�W�����t�B���^
					 {-1,5,-1},
					 {0,-1,0}};


void	get_data (int  agc, char  **agv){
  	if (agc != 2){
		printf ("�g�p�@:: %s  file.bmp\n", agv[0]);		exit(-1);
	}
	loadbmpfile(agv[1], &cm, &hite, &wide, dat);
}

void	calc_diff (void){
	int		x, y, j, k;		   
  	for (y=1; y<hite-1; y++) for (x=1; x<wide-1; x++){	
		ddw = 0;
  		for (j=-1; j<2; j++) for (k=-1; k<2; k++){
				
			
				ddw += gp(y+j, x+k)*wline[j+1][k+1];	// �G�b�W�����t�B���^��������B�����Ń}�C�i�X��255�ȏ�̒l���o�Ă��܂��B
			
			
  		}
		
  		dw[y][x] = ddw;//x,y�S�Ẳ�f���ɑ���B
			
		
  	}
}

void show(void){
	unsigned  char	gr;
	signed int ddy;//sighed�͂��Ȃ��Ă��ǂ����A�}�C�i�X�̒l����邱�Ƃ��킩��₷�����邽�߂ɂ��Ă���B
	int				x, y;
	glclear ( );
    for (y=0; y<hite; y++) for (x=0; x<wide; x++){
		gr = gp(y, x);			//���摜�̕\��
		glcolor (gr, gr, gr);	gpnt (x, y);
	}
    for (y=1; y<hite-1; y++) for (x=1; x<wide-1; x++){

		ddy = dw[y][x];//ddy�ɒu�������Ă���B
		//printf ("%d\n",ddy);


		/*�G�b�W���������邱�ƂŒl���}�C�i�X�ɂȂ�����255�ȏ�ɂȂ��Ă��܂��ꍇ������B
		�@������@�i���̒l�ɖ߂��j�A�܂��͇A�i0��255�̂ǂ��炩��������j���Ƃ��s���ƃm�C�Y�������ł���B
		*/
		if(ddy > 255){
			//glcolor (gr,gr,gr);//�@
			glcolor (255,255,255);//�A
		}else if(ddy  < 0){
			//glcolor (gr,gr,gr);//�@
			glcolor (0,0,0);//�A
		}else{		
			glcolor (ddy ,ddy ,ddy );
		}
		
			gpnt (x-wide, y);	
	}
	glflush ( );
}

void	main (int  argc, char  **argv){
	get_data (argc, argv);
	calc_diff ( );
	ginit (1);
	gdisplay (show);
}