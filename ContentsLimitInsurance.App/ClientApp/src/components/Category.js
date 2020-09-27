import React from "react";
import "./Category.css";
import HighValueItem from "./HighValueItem";

const Category = ({ categoryName, items, onRemovePressed }) => (
  <div className="list-wrapper">
    <h3>{categoryName}</h3>
    {items.map((item) => (
      <HighValueItem
        key={item.highValueItemId}
        item={item}
        onRemovePressed={onRemovePressed}
      />
    ))}
    <h3>
      Total: $
      {items.reduce(function (prev, current) {
        return prev + +current.value;
      }, 0)}
    </h3>
  </div>
);

export default Category;
