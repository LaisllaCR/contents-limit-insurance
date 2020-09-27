import React from "react";
import { connect } from "react-redux";
import "./Category.css";
import HighValueItem from "./HighValueItem";
import { removeHighValueItemRequest } from "../store/thunks";

const Category = ({ categoryName, items, onRemovePressed }) => (
  <div className="list-wrapper">
    <h3>
      {categoryName} $
      {items.reduce(function (prev, current) {
        return prev + +current.value;
      }, 0)}
    </h3>
    {items.map((item) => (
      <HighValueItem
        key={item.highValueItemId}
        item={item}
        onRemovePressed={onRemovePressed}
      />
    ))}
  </div>
);

const mapStateToProps = (state) => ({});

const mapDispatchToProps = (dispatch) => ({
  onRemovePressed: (highValueItemId) =>
    dispatch(removeHighValueItemRequest(highValueItemId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(Category);
