SteviloZvezd = 1;
SteviloPresledkov = 50;
Vrstice=0;

StevecEna = 0;
func Presledki(){
  if(StevecEna==SteviloPresledkov){

 } else {
  izpisi(" ");
  StevecEna = StevecEna + 1;
  Presledki();
 } 
}

StevecDve = 0;
func Zvezde(){
  if(StevecDve==SteviloZvezd){

 } else {
  izpisi("*");
  StevecDve = StevecDve + 1;
  Zvezde();
 } 
}

func Rekurzija(){
  if(Vrstice==51){
   
 } else {
  Presledki();
  Zvezde();
  Vrstice = Vrstice + 1;
  SteviloPresledkov = SteviloPresledkov - 1;
  SteviloZvezd = SteviloZvezd + 2;
  StevecEna = 0;
  StevecDve = 0;
  izpisi("\n");
  Rekurzija();
 } 
}



Rekurzija();
