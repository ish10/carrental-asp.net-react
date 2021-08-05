import Streams from '../api/Streams'

export const register=(FormValues)=>{
return async(dispatch, getState)=>{
    const response = await Streams.post('/api/Account',
    
    {...FormValues}
    );
    
    dispatch({type: 'SIGN_IN', payload: response.data });
}

}