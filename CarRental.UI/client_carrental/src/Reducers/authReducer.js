const Initial_State={
    isSignedIn: null,
    Token: null
};

export default (state=Initial_State,action)=>{
switch(action.type){
case  'SIGN_IN':
    return{...state,isSignedIn:true,Token:action.payload};
 default:
     return state;   


}



};