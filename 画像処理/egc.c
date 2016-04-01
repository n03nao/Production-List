/* 平滑化　フィルタ */
#include <windows.h>
#include	<stdio.h>
#include	"opgls.ft"

#define		MaxSiz		1024
#define		gp(y, x)	dat[(y)*wide+(x)]

unsigned  char	dat[MaxSiz*MaxSiz];
int	 			cm, wide, hite;
signed int		dw[MaxSiz][MaxSiz];//sighedはつけなくても良いが、マイナスの値も取ることをわかりやすくするためにしている。
signed int		ddw;	//sighedはつけなくても良いが、マイナスの値も取ることをわかりやすくするためにしている。


int		wline[3][3]={{0,-1,0},			// エッジ強調フィルタ
					 {-1,5,-1},
					 {0,-1,0}};


void	get_data (int  agc, char  **agv){
  	if (agc != 2){
		printf ("使用法:: %s  file.bmp\n", agv[0]);		exit(-1);
	}
	loadbmpfile(agv[1], &cm, &hite, &wide, dat);
}

void	calc_diff (void){
	int		x, y, j, k;		   
  	for (y=1; y<hite-1; y++) for (x=1; x<wide-1; x++){	
		ddw = 0;
  		for (j=-1; j<2; j++) for (k=-1; k<2; k++){
				
			
				ddw += gp(y+j, x+k)*wline[j+1][k+1];	// エッジ強調フィルタをかける。ここでマイナスや255以上の値が出てしまう。
			
			
  		}
		
  		dw[y][x] = ddw;//x,y全ての画素分に代入。
			
		
  	}
}

void show(void){
	unsigned  char	gr;
	signed int ddy;//sighedはつけなくても良いが、マイナスの値も取ることをわかりやすくするためにしている。
	int				x, y;
	glclear ( );
    for (y=0; y<hite; y++) for (x=0; x<wide; x++){
		gr = gp(y, x);			//原画像の表示
		glcolor (gr, gr, gr);	gpnt (x, y);
	}
    for (y=1; y<hite-1; y++) for (x=1; x<wide-1; x++){

		ddy = dw[y][x];//ddyに置き換えている。
		//printf ("%d\n",ddy);


		/*エッジ強調をすることで値がマイナスになったり255以上になってしまう場合がある。
		　これを①（元の値に戻す）、または②（0か255のどちらかを代入する）ことを行うとノイズ処理ができる。
		*/
		if(ddy > 255){
			//glcolor (gr,gr,gr);//①
			glcolor (255,255,255);//②
		}else if(ddy  < 0){
			//glcolor (gr,gr,gr);//①
			glcolor (0,0,0);//②
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