## FlowSynx MD5 Hashing Plugin

The MD5 Hashing Plugin is a lightweight, plug-and-play integration component for the FlowSynx engine. It provides MD5 hashing capabilities for strings and binary data within your automation workflows. Designed for FlowSynx’s no-code/low-code environment, this plugin simplifies the process of generating MD5 hashes for various input types.

This plugin is automatically installed by the FlowSynx engine when selected within the platform. It is not intended for manual installation or standalone developer use outside the FlowSynx environment.

---

## Purpose

The MD5 Hashing Plugin allows FlowSynx users to:

- Generate MD5 hashes from plain text or binary input.
- Use MD5 hashes for validation, integrity checks, or as part of larger workflow automation processes.
- Avoid writing custom code to implement hashing logic in workflows.

---

## Plugin Specifications

This plugin does not require any external configuration.

---

## Input Parameters

Each operation accepts specific parameters:

### Hash
| Parameter     | Type        | Required | Description                                                    |
|---------------|-------------|----------|----------------------------------------------------------------|
| `InputText`   | string      | No       | A UTF-8 string to compute the MD5 hash for.                   |
| `InputBytes`  | byte array  | No       | A byte array to compute the MD5 hash for.                     |

⚠️ **Note**: Either `InputText` **or** `InputBytes` must be provided. If both are provided, `InputBytes` takes precedence.

---

### Example Input

```json
{
  "Operation": "hash",
  "InputText": "Hello, FlowSynx!"
}
```

or

```json
{
  "Operation": "hash",
  "InputBytes": [72, 101, 108, 108, 111]
}
```

---

### Example Output

```json
{
  "Hash": "8b1a9953c4611296a827abf8c47804d7"
}
```

---

## Debugging Tips

- Ensure that either `InputText` or `InputBytes` is provided in the input payload.
- If both parameters are provided, verify that you intended to prioritize `InputBytes`.
- Double-check UTF-8 encoding when working with `InputText` to avoid mismatched hash results.

---

## Security Notes

- This plugin performs MD5 hashing only. It does **not** store or transmit input or output data outside the FlowSynx engine’s execution context.
- MD5 is considered **weak for cryptographic security**. Use it only for non-critical purposes such as checksums or basic integrity verification.

---

## License

© FlowSynx. All rights reserved.