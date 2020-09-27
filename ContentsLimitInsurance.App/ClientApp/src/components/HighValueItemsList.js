import React, { useEffect } from "react";
import { connect } from "react-redux";
import NewHighValueItemForm from "./NewHighValueItemForm";
import HighValueItem from "./HighValueItem";
import "./HighValueItemsList.css";
import {
  loadHighValueItems,
  removeHighValueItemRequest,
} from "../store/thunks";

const HighValueItemsList = ({
  items = [],
  onRemovePressed,
  isLoading,
  startLoadingItems,
}) => {
  useEffect(() => {
    startLoadingItems();
  }, []);

  const loadingMessage = <div>Loading highValueItems...</div>;
  const content = (
    <div className="list-wrapper">
      <NewHighValueItemForm />
      {items.map((highValueItem) => (
        <HighValueItem
          key={highValueItem.highValueItemId}
          item={highValueItem}
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
  return isLoading ? loadingMessage : content;
};

const mapStateToProps = (state) => ({
  isLoading: state.isLoading,
  items: state.items,
});

const mapDispatchToProps = (dispatch) => ({
  startLoadingItems: () => dispatch(loadHighValueItems()),
  onRemovePressed: (highValueItemId) =>
    dispatch(removeHighValueItemRequest(highValueItemId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(HighValueItemsList);
