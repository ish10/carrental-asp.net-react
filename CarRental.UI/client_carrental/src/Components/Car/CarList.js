import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { getCars, deleteCar, updateCar } from "../../Actions";

const CarList = (props) => {
  // useEffect(()=>{
  //   props.getCars()
  //  },[])
  const onDeleteCar=(id)=>{
    props.deleteCar(id)
    console.log(`car with id ${id} has now been deleted`)
  }

  const carList = () => {
    if (props.cars.length > 1) {
      console.log(props.cars);
      return props.cars.map((x) => {
        return (
          <div key={x.carId}>
            {" "}
            {x.pricePerDay} {x.image} {x.numberPlate} {x.location.city}{" "}
            {x.location.province}<br></br>
            <button onClick={(e)=>{onDeleteCar(x.carId)}}>Delete</button>
          </div>
        );
      });
    }
  };
  return (
    <div>
      <h1> Car List</h1>
      {carList()}
    </div>
  );
};

const mapStateToProps = (state, ownProps) => {
  return { cars: state.cars };
};

export default connect(mapStateToProps, { getCars, deleteCar, updateCar })(
  CarList
);
