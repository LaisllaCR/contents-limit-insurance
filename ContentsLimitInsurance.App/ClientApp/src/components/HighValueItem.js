import React from "react";
import { Button } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";

const HighValueItem = ({ item, onRemovePressed }) => (
  <tr class="d-flex">
    <td class="col-sm-9">{item.name}</td>
    <td class="col-sm-1">${item.value}</td>
    <td class="col-sm-1">
      <Button
        variant="danger"
        onClick={() => onRemovePressed(item.highValueItemId)}
      >
        <FontAwesomeIcon icon={faTrash} />
      </Button>
    </td>
  </tr>
);

export default HighValueItem;
