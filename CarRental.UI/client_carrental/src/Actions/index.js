import Streams from '../api/Streams'



export const logIn=(LoginDetails)=>{
    return async(dispatch,getState)=>{
         const response = await Streams.post('/api/Account/login',
        
        {...LoginDetails}
        );
      console.log(response.data );
        dispatch({type: 'LOG_IN', payload: response.data });
    };
};
    export const register=(FormValues)=>{
      return async(dispatch, getState)=>{
    const response = await Streams.post('/api/Account/register',
    
    {...FormValues}
    );
    
    dispatch({type: 'SIGN_IN', payload: response.data });
};

};
