import React, { useState } from "react";
import Select from "react-select";
import { connect } from "react-redux";
import { createCar } from "../../Actions/index";

const AddCar = (props) => {
  const [formValues, setFormValues] = useState({
    carId: 0,
    model: 0,
    pricePerDay: 0,
    image: '',
    numberPlate: '',
    locationId:0,
    city:'',
    province:0
    // location:{
    //     locationId:0,
    //     city:'',
    //     province:0
    // }
  });

  const modelOptions = [
    { label: "Economy", value: 0 },
    { label: "Compact", value: 1 },
    { label: "Intermediate", value: 2 },
    { label: "Standard", value: 3 },
    { label: "Convertible", value: 4 },
    { label: "Sporty", value: 5 },
    { label: "Premium", value: 6 },
    { label: "Luxury", value: 7 }
  ];

  const provinceOptions = [
    { label: "Alberta", value: 0 },
    { label: "BritishColumbia", value: 1 },
    { label: "Manitoba", value: 2 },
    { label: "NewBrunswick", value: 3 },
    { label: "NewfoundlandAndLabrador", value: 4 },
    { label: "NorthwestTerritories", value: 5 },
    { label: "NovaScotia", value: 6 },
    { label: "Nunavut", value: 7 },
    { label: "Ontario", value: 8 },
    { label: "PrinceEdwardIsland", value: 9 },
    { label: "Quebec", value: 10 },
    { label: "Saskatchewan", value: 11 },
    { label: "Yukon", value: 12 }
  ];

  const onFormSubmit = (event) => {
    event.preventDefault();
    props.createCar(formValues);
  };


  return (
    <div>
      <form onSubmit={(e) => onFormSubmit(e)}>
      <br></br>
        <label>Model</label>
        <Select
          options={modelOptions}
          value={formValues.model}
          defaultValue={formValues.model}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              model: e.value,
            }))
          }
        ></Select>

        <br></br>
        <label>Price per day</label>
        <input
          type="number"
          value={formValues.pricePerDay}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              pricePerDay: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>Image URL</label>
        <input
          type="text"
          value={formValues.image}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              image: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>Plate Number</label>
        <input
          type="text"
          value={formValues.numberPlate}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              numberPlate: e.target.value,
            }))
          }
        ></input>

        <br></br>
        <label>City Name</label>
        <input
          type="text"
          value={formValues.city}
          onChange={(e) =>
            setFormValues((obj) => ({
              ...obj,
              city: e.target.value,
            }))
          }
        ></input>
        
        <br></br>
        <label>Province Name</label>
        <Select
          options={provinceOptions}
          value={formValues.province}
          onChange={(e) =>
             setFormValues((obj) => ({
               ...obj,
               province: e.value,
             }))
          }
        ></Select>

        <button onClick={(event)=>onFormSubmit(event)}>Submit</button>
      </form>
    </div>
  );
};

export default connect(null,{createCar}) (AddCar)