/* 平滑化　フィルタ */
#include <windows.h>
#include	<stdio.h>
#include	"opgls.ft"

#define		MaxSiz		1024
#define		gp(y, x)	dat[(y)*wide+(x)]

unsigned  char	dat[MaxSiz*MaxSiz];
int	 			cm, wide, hite;
int				dn[MaxSiz][MaxSiz],
				dw[MaxSiz][MaxSiz];

void	get_data (int  agc, char  **agv){
  	if (agc != 2){
		printf ("使用法:: %s  file.bmp\n", agv[0]);		exit(-1);
	}
	loadbmpfile(agv[1], &cm, &hite, &wide, dat);
}


void show(void){
	unsigned  char	gr, ddx, ddy,r;
	int				x, y, i, j, s, t, v, max;
	int             n[9];

	glclear ( );
    for (y=0; y<hite; y++) for (x=0; x<wide; x++){
		gr = gp(y, x);			//原画像の表示
		glcolor (gr, gr, gr);	gpnt (x, y);
	}

    for (y=1; y<hite-1; y++) for (x=1; x<wide-1; x++){

		gr = gp(y, x);		
		glcolor (gr, gr, gr);   gpnt (x-wide, y);	//原画像の表示
		//glcolor (gr, gr, gr);   gpnt (x-wide, y);
		r = dat[y*wide+x];

		/*
		n[0]=gp(y-1,x-1);
		n[1]=gp(y,x-1);
		n[2]=gp(y+1,x-1);
		n[3]=gp(y-1,x);
		n[4]=gp(y,x);
		n[5]=gp(y+1,x);
		n[6]=gp(y-1,x+1);
		n[7]=gp(y,x+1);
		n[8]=gp(y+1,x+1);
		*/

		s=0;
		for (i=-1; i<=1; i++) for (j=-1; j<=1; j++){
			if(s<9){
				n[s] = gp(y+j,x+i);
			}
			s++;
		}
	
		for(t=0; t<8; t++) for(v=t+1; v<9; v++){//大きい順に並べる
			if(n[t] >n[v]){
				max = n[t];
				n[t] = n[v];
				n[v] = max;
			}
		}	

		gp(y,x) = n[4];
		gr = gp(y, x);	
		
		glcolor (gr,gr,gr);	gpnt (x-wide, y);

		//qsort(n,9,sizeof(int),(int(*)(const void*,const void*))compare_int);
	}
	glflush ( );
}

void	main (int  argc, char  **argv){
	get_data (argc, argv);
	ginit (1);
	gdisplay (show);
}