import React from "react";
import "./HighValueItem.css";

const HighValueItem = ({ item, onRemovePressed }) => (
  <div className="todo-item-container">
    <h3>{item.name}</h3>
    <h4>${item.value}</h4>
    <div className="buttons-container">
      <button
        onClick={() => onRemovePressed(item.highValueItemId)}
        className="remove-button"
      >
        Remove
      </button>
    </div>
  </div>
);

export default HighValueItem;
