float rx = 0.0;  // x軸まわりの回転角度
float ry = 0.0;  // y軸まわりの回転角度
float rz = 0.0;  // z軸まわりの回転角度

String file_name="save-11.jpg"; 
float[][][] Py; // Model
float[] Light= {
  -1, 0, -0.2
};
float[] col= {
  1, 1, 1
};

void setup() {
  size(800, 800, P3D);  // 3Dモード
  noFill();             // 塗り潰し（なしモード）
  noStroke();          // 輪郭線色（白）
  Py= new float[264][3][3];// モデル領域の生成
  TrmSphere(Py, 12, 0.8); // モデルの生成
  float l= sqrt(Light[0]*Light[0]+Light[1]*Light[1]+Light[2]*Light[2]);
  Light[0]/=l; 
  Light[1]/=l; 
  Light[2]/=l;
}

void draw() {
  // 画面をリフレッシュ
  background(128);    // 背景（灰色）
  ortho(-width/2, width/2, -height/2, height/2, -10, 10);  // 並行投影  

  translate(width/2, height/2, 0); // 画面の中心に原点を移動
  rotateX(rx);   // x軸まわりの回転
  rotateY(ry);   // y軸まわりの回転
  rotateZ(rz);   // z軸まわりの回転
  scale(250);    // 250倍に拡大
  translate(0, 0, 0); //の中心に移動

  // 立体を描画
  // draw_triangle(Py, 160);  // サンプル
  
  //setcolor(1, 0, 0);
  //rend_triangle(Py, 264);
  
  scale(0.5,0.5,0.5);
  setcolor(1,0,0);
  rend_triangle(Py,264);
  setcolor(0,1,0);
  translate(2,0,0);
  rend_triangle(Py,264);
  setcolor(0,0,1);
  translate(-4,0,0);
  rend_triangle(Py,264);
  
  
}

void draw_triangle(float [][][] Tr, int num) {
  //  (演習２－２) 
  for (int n=0; n<=num-1; n++) {
    beginShape(TRIANGLES);
    vertex(Tr[n][0][0], Tr[n][0][1], Tr[n][0][2]);
    vertex(Tr[n][1][0], Tr[n][1][1], Tr[n][1][2]);
    vertex(Tr[n][2][0], Tr[n][2][1], Tr[n][2][2]);
    endShape();
  }
}

void rend_triangle(float[][][] Tr, int num) {
  int n;
  float[] NV; 
  float br;
  NV = new float[3];
  for (n=0; n<num; n++) {
   
    
    
    
    
    
    Normal(Tr[n], NV);
    br=(NV[0]*Light[0]+NV[1]*Light[1]+NV[2]*Light[2])*255;
    fill(col[0]*br, col[1]*br, col[2]*br);
    beginShape(TRIANGLES);
    vertex();
    vertex(Tr[n][0][0], Tr[n][0][1], Tr[n][0][2]);
    vertex(Tr[n][1][0], Tr[n][1][1], Tr[n][1][2]);
    vertex(Tr[n][2][0], Tr[n][2][1], Tr[n][2][2]);
    endShape();
  }
}

void Normal(float[][] Tr, float[] N) {
  float r;
  float[] D1, D2;
  D1 = new float[3]; 
  D2 = new float[3];
  D1[0]=Tr[1][0]-Tr[0][0];
  D1[1]=Tr[1][1]-Tr[0][1];
  D1[2]=Tr[1][2]-Tr[0][2];

  D2[0]=Tr[2][0]-Tr[1][0];
  D2[1]=Tr[2][1]-Tr[1][1];
  D2[2]=Tr[2][2]-Tr[1][2];

  N[0]=D1[1]*D2[2]-D1[2]*D2[1]; 
  N[1]=D1[2]*D2[0]-D1[0]*D2[2];
  N[2]=D1[0]*D2[1]-D1[1]*D2[0];
  r=sqrt(N[0]*N[0]+N[1]*N[1]+N[2]*N[2]);
  N[0]/=r;
  N[1]/=r; 
  N[2]/=r;
}

void setcolor(float r, float g, float b) {
  col[0]=r; 
  col[1]=g; 
  col[2]=b;
}

void cal_t_point(float R,  float t, float p, float[] pn) {
  float td=t*3.141592/180; 
  float pd=p*3.141592/180;
  pn[0]=(R*cos(pd))*cos(td);
  pn[1]=(R*cos(pd))*sin(td);
  pn[2]=R*sin(pd);
}

void TrmSphere(float[][][] tr, int num, float R)
{
  //float ddt=2.0*3.1415/num, dt;
  float d=360/num;  //横の分割
  int t=0;
  int p=-(360/num)*2;
  int n=0;
  int m=96;
  int b=8;

  for (int i=0;i<num-b;i++) {
    for (int j=0;j<num;j++)
    {
      //四角形の生成
      cal_t_point(R, t, p, tr[n][0]);
      cal_t_point(R, t+d, p+d, tr[n][1]);
      cal_t_point(R, t, p+d, tr[n][2]); 
      
      cal_t_point(R, t, p, tr[n+num*(num-b)][0]);
      cal_t_point(R, t+d, p, tr[n+num*(num-b)][1]);
      cal_t_point(R, t+d, p+d, tr[n+num*(num-b)][2]);
      
      //球の上・下部の三角形の生成
      cal_t_point(R, t, 180/2+d, tr[m][0]);
      cal_t_point(R, t+d, 180/2+d, tr[m][1]);
      cal_t_point(R, t, 180/2, tr[m][2]); 
      
      cal_t_point(R, t, -180/2+d, tr[m+num][0]);
      cal_t_point(R, t, -180/2, tr[m+num][1]);
      cal_t_point(R, t+d, -180/2+d, tr[m+num][2]);
      
      m++;
      n++;
     
      t +=d;
      
    }
    p+=d;
  }
  
 println(p);
 println(t);
  
  /*
  for(int k=0;k<num;k++){
      //球の上・下部の三角形の生成
      cal_t_point(R, t, 180/2, tr[m][0]);
      cal_t_point(R, t+d, 180/2-d, tr[m][1]);
      cal_t_point(R, t+d, 180/2-d, tr[m][2]); 
      
      cal_t_point(R, t, -180/2+d, tr[m+num][0]);
      cal_t_point(R, t, -180/2, tr[m+num][1]);
      cal_t_point(R, t+d, -180/2+d, tr[m+num][2]);
      
      m++;
      t+=d;
  }
  */

  // for(i=0;i<num;i++){
  // //dt=i*ddt;
  //
  // tr[i][0][0]=r*sin(dt);   tr[i][0][1]=r*cos(dt);   tr[i][0][2]=0;
  // tr[i][1][0]=r*sin(dt+ddt);tr[i][1][1]=r*cos(dt+ddt);tr[i][1][2]=0;
  // tr[i][2][0]=0;   tr[i][2][1]=0;   tr[i][2][2]=h;
  // };
  // for(int j=0;j<num;j++){
  // i=j+num;
  // dt=j*ddt;
  // tr[i][0][0]=r*sin(dt);   tr[i][0][1]=r*cos(dt);   tr[i][0][2]=0;
  // tr[i][1][0]=r*sin(dt+ddt);tr[i][1][1]=r*cos(dt+ddt);tr[i][1][2]=0;
  // tr[i][2][0]=0;   tr[i][2][1]=0;   tr[i][2][2]=0;
  // };
}

/*
// 以下ｈ、４．４で説明
 void TrmPyramid(float[][][] tr, int num, float h, float r)
 {
 float ddt=2.0*3.1415/num, dt;
 int i;
 for(i=0;i<num;i++){
 dt=i*ddt;
 tr[i][0][0]=r*sin(dt);   tr[i][0][1]=r*cos(dt);   tr[i][0][2]=0;
 tr[i][1][0]=r*sin(dt+ddt);tr[i][1][1]=r*cos(dt+ddt);tr[i][1][2]=0;
 tr[i][2][0]=0;   tr[i][2][1]=0;   tr[i][2][2]=h;
 };
 for(int j=0;j<num;j++){
 i=j+num;
 dt=j*ddt;
 tr[i][0][0]=r*sin(dt);   tr[i][0][1]=r*cos(dt);   tr[i][0][2]=0;
 tr[i][1][0]=r*sin(dt+ddt);tr[i][1][1]=r*cos(dt+ddt);tr[i][1][2]=0;
 tr[i][2][0]=0;   tr[i][2][1]=0;   tr[i][2][2]=0;
 };
 }
 */
/*
void TrmCylinder(float[][][] tr, int num, float h, float r) { 
 
 //  この部分を完成させなさい（演習２－３）
 float ddt=2.0*3.1415/num, dt;
 int i, s, j, t;
 for (i=0;i<num;i++) {
 dt=i*ddt;
 tr[i][0][0]=r*sin(dt);   
 tr[i][0][1]=r*cos(dt);   
 tr[i][0][2]=0;
 tr[i][1][0]=r*sin(dt+ddt);
 tr[i][1][1]=r*cos(dt+ddt);
 tr[i][1][2]=0;
 tr[i][2][0]=r*sin(dt+ddt);   
 tr[i][2][1]=r*cos(dt+ddt);   
 tr[i][2][2]=h;
 };
 for (s=0;s<num;s++) {
 i= s+num;
 dt=s*ddt;
 tr[i][0][0]=r*sin(dt);   
 tr[i][0][1]=r*cos(dt);   
 tr[i][0][2]=0;
 tr[i][1][0]=r*sin(dt+ddt);
 tr[i][1][1]=r*cos(dt+ddt);
 tr[i][1][2]=h;
 tr[i][2][0]=r*sin(dt);   
 tr[i][2][1]=r*cos(dt);   
 tr[i][2][2]=h;
 };
 for (j=0;j<num;j++) {
 i =j+num*2;
 dt=j*ddt;
 tr[i][0][0]=r*sin(dt);   
 tr[i][0][1]=r*cos(dt);   
 tr[i][0][2]=0;
 tr[i][1][0]=r*sin(dt+ddt);
 tr[i][1][1]=r*cos(dt+ddt);
 tr[i][1][2]=0;
 tr[i][2][0]=0;   
 tr[i][2][1]=0;   
 tr[i][2][2]=0;
 };
 for (t=0;t<num;t++) {
 i =t+num*3;
 dt=t*ddt;
 tr[i][0][0]=r*sin(dt); 
 tr[i][0][1]=r*cos(dt); 
 tr[i][0][2]=h;
 tr[i][1][0]=r*sin(dt+ddt);
 tr[i][1][1]=r*cos(dt+ddt);
 tr[i][1][2]=h;
 tr[i][2][0]=0;   
 tr[i][2][1]=0;   
 tr[i][2][2]=h;
 };
 }*/



// キー操作，立体回転(矢印)とフレーム画像格納(s)
void keyPressed() {
  if (keyCode == UP) { 
    rx = rx + 0.1;
  } // rotation angle
  else if (keyCode == DOWN) { 
    rx = rx - 0.1;
  }
  else if (keyCode == LEFT) { 
    rz = rz - 0.1;
  }
  else if (keyCode == RIGHT) { 
    rz = rz + 0.1;
  }
  else if ( key== 's' ) { 
    saveFrame(file_name);
  }  // Save File Name
}

